'use client'

import {useEffect, useState} from "react";
import {InputText} from "primereact/inputtext";
import {Calendar} from "primereact/calendar";
import {Button} from "primereact/button";

const ContactForm = ({ contact, onSave }) => {
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        city: { name: '' },
        gender: '',
        birthDate: null,
        ...contact
    });

    useEffect(() => {
        if (contact) {
            setFormData(contact);
        }
    }, [contact]);

    const handleChange = (e) => {
        const { id, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [id]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (onSave) {
                await onSave(formData);
            }
        } catch (error) {
            console.error("Error saving contact:", error);
        }
    };

    return (
        <form className="p-fluid" onSubmit={handleSubmit}>
            <div className="field">
                <label htmlFor="firstName">First Name</label>
                <InputText id="firstName" type="text" value={formData.firstName} onChange={handleChange} />
            </div>
            <div className="field">
                <label htmlFor="lastName">Last Name</label>
                <InputText id="lastName" type="text" value={formData.lastName} onChange={handleChange} />
            </div>
            <div className="field">
                <label htmlFor="email">Email</label>
                <InputText id="email" type="email" value={formData.email} onChange={handleChange} />
            </div>
            <div className="field">
                <label htmlFor="phoneNumber">Phone Number</label>
                <InputText id="phoneNumber" type="text" value={formData.phoneNumber} onChange={handleChange} />
            </div>
            <div className="field">
                <label htmlFor="city">City</label>
                <InputText id="city" type="text" value={formData.city.name} onChange={(e) => setFormData(prevData => ({
                    ...prevData,
                    city: { name: e.target.value }
                }))} />
            </div>
            <div className="field">
                <label htmlFor="gender">Gender</label>
                <InputText id="gender" type="text" value={formData.gender} onChange={handleChange} />
            </div>
            <div className="field">
                <label htmlFor="birthDate">Birth Date</label>
                <Calendar id="birthDate" value={formData.birthDate} onChange={(e) => setFormData(prevData => ({
                    ...prevData,
                    birthDate: e.value
                }))} selectionMode='single' />
            </div>
            <Button label="Save" icon="pi pi-check" type="submit" />
        </form>
    );
};

export default ContactForm;
