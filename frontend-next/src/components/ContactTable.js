'use client'

import React, {useEffect, useState,} from 'react';
import {DataTable} from 'primereact/datatable';
import {Column} from 'primereact/column';
import {ContactsApi, Gender} from "@/services/api";
import {OpenApiConfig} from "@/services/OpenApiConfig";
import {EnumMapping} from "@/components/shared/EnumMapping";

const ContactTable = () => {
    const [loading, setLoading] = useState(true);
    const [totalRecords, setTotalRecords] = useState(0);
    const [contacts, setContacts] = useState([]);
    const [rows, setRows] = useState(10);
    const [first, setFirst] = useState(0);
    const [orderColumn, setOrderColumn] = useState(null); //ascending or descending
    const [page, setPage] = useState(0);
    const [pageSize, setPageSize] = useState(10);
    const [sortField, setSortField] = useState(null);
    const [sortOrder, setSortOrder] = useState(1);


    const [globalFilter, setGlobalFilter] = useState('');

    const fetchContacts = async () => {
        setLoading(true);
        try {

            const requestParameters = {
                contactsRequest: {
                    skip: page * pageSize,
                    take: pageSize,
                    orderBy: orderColumn,
                    orderDescending: sortOrder === -1,
                }
            };

            const response = await new ContactsApi(OpenApiConfig.getConfig())
                .apiContactsContactsPost(requestParameters)

            console.log(response);

            setContacts(response.data);
            setTotalRecords(response.count);
        } catch (error) {
            console.error("Error fetching contacts", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchContacts(0, rows);
    }, [rows]);


    const onPageChange = (event) => {
        setLoading(true);
        console.log(event);
        setFirst(event.first);
        setRows(event.rows);
        fetchContacts(event.page, event.rows);
    };

    const onSortChange = async (event) => {
        console.log(event);
        const newSortOrder = event.sortField === sortField ? -sortOrder : 1;
        setSortField(event.sortField);
        setSortOrder(newSortOrder);
        setLoading(true);
       await  fetchContacts();

    }

    return (
        <div className="p-fluid">
            {/*<InputText
                type="search"
                onInput={(e) => setGlobalFilter(e.target.value)}
                placeholder="Cerca..."
            />*/}

            <DataTable
                value={contacts}
                paginator
                rows={rows}
                stripedRows
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
                globalFilter={globalFilter}
                emptyMessage="Nessun contatto trovato"

                className="p-mt-4"
            >
                <Column field="firstName" header="Nome" sortable filterField="firstName"/>
                <Column field="lastName" header="Cognome" sortable/>
                <Column field="email" header="Email" sortable/>
                <Column field="phoneNumber" header="Telefono"/>
                <Column field="city.name" header="Città " sortable/>
                <Column field="birthDate" header="Data di nascita" sortable body={(rowData) => {
                    const date = new Date(rowData.birthDate);
                    return date.toLocaleDateString("it-IT");
                }}/>
                <Column field="gender" header="Sesso" sortable body={(rowData) => {
                    return EnumMapping.getGenderLabel(rowData.gender)
                }}/>
            </DataTable>
        </div>
    );
};

export default ContactTable;
