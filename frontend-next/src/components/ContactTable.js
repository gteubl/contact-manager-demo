'use client'

import React, {useEffect, useRef, useState,} from 'react';
import {DataTable} from 'primereact/datatable';
import {Column} from 'primereact/column';
import {ContactsApi, Gender} from "@/services/api";
import {OpenApiConfig} from "@/services/OpenApiConfig";
import {EnumMapping} from "@/components/shared/EnumMapping";
import {InputIcon} from "primereact/inputicon";
import {InputText} from "primereact/inputtext";
import {IconField} from "primereact/iconfield";
import {Button} from "primereact/button";
import ContactForm from "@/components/ContactForm";
import {Dialog} from "primereact/dialog";
import {Toast} from "primereact/toast";
import {debounce} from "lodash";

const ContactTable = () => {
    const [loading, setLoading] = useState(true);
    const [totalRecords, setTotalRecords] = useState(0);
    const [contacts, setContacts] = useState([]);
    const [rows, setRows] = useState(10);
    const [first, setFirst] = useState(0);
    const [orderColumn, setOrderColumn] = useState(null); //ascending or descending
    const [page, setPage] = useState(0);
    const [sortField, setSortField] = useState(null);
    const [sortOrder, setSortOrder] = useState(1);
    const [magicFilter, setMagicFilter] = useState('');
    const [debouncedMagicFilter, setDebouncedMagicFilter] = useState('');
    const [editModalVisible, setEditModalVisible] = useState(false);
    const [selectedContact, setSelectedContact] = useState(null);
    const [contactDialogHeader, setContactDialogHeader] = useState('Nuovo contatto');
    const toast = useRef(null);


    const columnsToFilter = ["firstName", "lastName", "email", "phoneNumber", "city.name"];

    const fetchContacts = async () => {
        setLoading(true);
        try {
            const requestParameters = {
                contactsRequest: {
                    skip: page * rows,
                    take: rows,
                    orderBy: sortField,
                    orderDescending: sortOrder === -1,
                    magicFilter: magicFilter,
                    columnsToFilter: columnsToFilter,

                }
            };

            const response = await new ContactsApi(OpenApiConfig.getConfig())
                .apiContactsContactsPost(requestParameters)

            setContacts(response.data);
            setTotalRecords(response.count);
        } catch (error) {
            console.error("Error fetching contacts", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchContacts();
    }, [page, rows, sortField, sortOrder, debouncedMagicFilter]);

    const debouncedFilter = useRef(
        debounce((value) => {
            setDebouncedMagicFilter(value);
        }, 300)
    ).current;

    const onChangeMagicFilter = (event) => {
        setMagicFilter(event.target.value);
        debouncedFilter(event.target.value);
    }

    const onPageChange = async (event) => {
        setPage(event.page);
        setFirst(event.first);
        setRows(event.rows);
    };

    const onSortChange = async (event) => {
        const newSortOrder = event.sortField === sortField ? -sortOrder : 1;
        setSortField(event.sortField);
        setSortOrder(newSortOrder);
    }


    const showSuccess = (message) => {
        toast.current.show({severity: 'success', summary: 'Successo', detail: message, life: 3000});
    }

    const showError = (message) => {
        toast.current.show({severity: 'error', summary: 'Errore', detail: message, life: 3000});
    }

    const onSaveContact = async (updatedContact) => {
        setEditModalVisible(false);
        setLoading(true);

        const requestParameters = {
            contactDto: {
                ...updatedContact
            }
        }
        try {
            if (updatedContact.id) {

                await new ContactsApi(OpenApiConfig.getConfig()).apiContactsUpdateContactsPut(requestParameters);
            } else {
                await new ContactsApi(OpenApiConfig.getConfig()).apiContactsCreateContactsPost(requestParameters);
            }
            showSuccess('Contatto salvato con successo');

            await fetchContacts();
        } catch (error) {
            showError('Errore durante il salvataggio del contatto');
        } finally {
            setLoading(false);
        }
    };

    const onDeleteContact = async (contact) => {
        setEditModalVisible(false);
        setLoading(true);
        const requestParameters = {
            id: contact.id
        }
        try {
            await new ContactsApi(OpenApiConfig.getConfig()).apiContactsDeleteContactsDelete(requestParameters)
            showSuccess('Contatto eliminato con successo');
            await fetchContacts();
        } catch (error) {

            showError('Errore durante l\'eliminazione del contatto');
        } finally {
            setLoading(false)
        }
    };


    const renderHeader = () => {
        return (

            <div className="grid align-items-center">
                <IconField iconPosition="left" className="col">
                    <InputIcon className="pi pi-search mr-1"/>
                    <InputText value={magicFilter} onChange={onChangeMagicFilter} placeholder="Filtra risultati..."/>
                </IconField>

                <Button icon="pi pi-plus" rounded text raised aria-label="plus"
                        onClick={() => {
                            setContactDialogHeader('Nuovo contatto');
                            setEditModalVisible(true);
                            setSelectedContact(null);
                        }}
                />
            </div>
        );
    };

    const editTemplate = (rowData) => {
        return (
            <Button icon="pi pi-pencil" rounded text raised aria-label="Search"
                    onClick={() => {
                        setContactDialogHeader('Modifica contatto');
                        setEditModalVisible(true);
                        setSelectedContact(rowData);
                    }}/>
        )
    }

    const header = renderHeader();


    return (
        <>
            <Toast ref={toast}/>
            <DataTable
                value={contacts}
                paginator
                rows={rows}
                stripedRows
                header={header}
                rowsPerPageOptions={[5, 10, 25, 50]}
                totalRecords={totalRecords}
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                sortField={sortField}
                sortOrder={sortOrder}
                lazy
                first={first}
                onPage={onPageChange}
                onSort={onSortChange}
                currentPageReportTemplate="Mostrando {first} a {last} di {totalRecords} contatti"
                loading={loading}
                emptyMessage="Nessun contatto trovato"
                className="p-mt-4"
            >
                <Column body={editTemplate} bodyStyle={{textAlign: 'center'}}></Column>
                <Column field="firstName" header="Nome" sortable filterField="firstName"/>
                <Column field="lastName" header="Cognome" sortable/>
                <Column field="email" header="Email" sortable/>
                <Column field="phoneNumber" header="Telefono"/>
                <Column field="city.name" header="Città " sortable/>
                <Column field="birthDate" header="Data di nascita" sortable body={(rowData) => {
                    const date = new Date(rowData.birthDate);
                    return date.toLocaleDateString("it-IT");
                }}/>
                <Column field="gender" header="Sesso" body={(rowData) => {
                    return EnumMapping.getGenderLabel(rowData.gender)
                }}/>
            </DataTable>

            <Dialog header={contactDialogHeader} visible={editModalVisible} style={{width: '50vw'}}
                    onHide={() => setEditModalVisible(false)}
            >
                <ContactForm contact={selectedContact} onSave={onSaveContact} onDelete={onDeleteContact}/>
            </Dialog>
        </>

    );
};

export default ContactTable;
