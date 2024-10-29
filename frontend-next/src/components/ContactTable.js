'use client'

import React, {useEffect, useState,} from 'react';
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
    }, []);

    useEffect(() => {
        const handler = setTimeout(() => {
            setDebouncedMagicFilter(magicFilter);
        }, 300);

        return () => {
            clearTimeout(handler);
        };
    }, [magicFilter]);

    useEffect(() => {
        fetchContacts();
    }, [debouncedMagicFilter]);


    const onPageChange = async (event) => {
        setLoading(true);
        setPage(event.page);
        setFirst(event.first);
        setRows(event.rows);
        await fetchContacts();
    };

    const onSortChange = async (event) => {
        const newSortOrder = event.sortField === sortField ? -sortOrder : 1;
        setSortField(event.sortField);
        setSortOrder(newSortOrder);
        setLoading(true);
        await fetchContacts();

    }
    const onChangeMagicFilter = (event) => {
        setMagicFilter(event.target.value);
    }

    const onSaveContact = async (updatedContact) => {
        setEditModalVisible(false);
        await fetchContacts();
    };




    const renderHeader = () => {
        return (

            <div className="grid align-items-center">
                <IconField iconPosition="left" className="col">
                    <InputIcon className="pi pi-search mr-1"/>
                    <InputText value={magicFilter} onChange={onChangeMagicFilter} placeholder="Filtra risultati..."/>
                </IconField>

                <Button icon="pi pi-plus" rounded text raised aria-label="plus"/>
            </div>
        );
    };

    const editTemplate = (rowData) => {
        return (
            <>
                <Button icon="pi pi-pencil" rounded text raised aria-label="Search"
                        onClick={() => {
                            setEditModalVisible(true);
                            setSelectedContact(rowData);
                        }

                        }/>

            </>
        )
    }

    const header = renderHeader();


    return (
        <>
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

            <Dialog header="Header" visible={editModalVisible} style={{width: '50vw'}}
                    onHide={() => setEditModalVisible(false)}
            >
                <ContactForm contact={selectedContact} onSave={onSaveContact} />
            </Dialog>
        </>

    );
};

export default ContactTable;
