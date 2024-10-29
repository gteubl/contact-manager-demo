'use client'

import {useEffect, useState} from "react";
import {InputText} from "primereact/inputtext";
import {Calendar} from "primereact/calendar";
import {Button} from "primereact/button";
import {EnumMapping} from "@/components/shared/EnumMapping";
import {Dropdown} from "primereact/dropdown";
import {CitiesApi, ContactsApi} from "@/services/api";
import {OpenApiConfig} from "@/services/OpenApiConfig";
import {InputMask} from "primereact/inputmask";

const ContactForm = ({contact, onSave}) => {
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        city: {name: ''},
        gender: null,
        birthDate: null,
        ...contact
    });

    const [cities, setCities] = useState([]);
    const [errors, setErrors] = useState({});

    const genderOptions = [
        {label: EnumMapping.getGenderLabel(1), value: 1},
        {label: EnumMapping.getGenderLabel(2), value: 2}
    ];

    useEffect(() => {
        if (contact) {
            setFormData(contact);
        }
    }, [contact]);

    useEffect(() => {
        fetchCity();
    }, []);

    const fetchCity = async (e) => {
        const response = await new CitiesApi(OpenApiConfig.getConfig())
            .apiCitiesCitiesGet()
        setCities(response);
    }

    const showSuccess = () => {
        toast.current.show({severity: 'success', summary: 'Success', detail: 'Message Content', life: 3000});
    }

    const handleGenderChange = (e) => {
        setFormData((prevData) => ({
            ...prevData,
            gender: e.value
        }));
    };

    const handlePhoneChange = (e) => {
        setFormData((prevData) => ({
            ...prevData,
            phoneNumber: e.target.value
        }));
    };

    const handleChange = (e) => {
        const {id, value} = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [id]: value
        }));
    };

    const validate = () => {
        const newErrors = {};

        if (!formData.firstName) newErrors.firstName = "Nome è obbligatorio";
        if (!formData.lastName) newErrors.lastName = "Cognome è obbligatorio";
        if (!formData.gender) newErrors.gender = "Genere è obbligatorio";
        if (!formData.email) {
            newErrors.email = "Email è obbligatorio";
        } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
            newErrors.email = "Email non è valido";
        }
        if (!formData.phoneNumber) {
            newErrors.phoneNumber = "Numero di telefono è obbligatorio";
        } else if (!/^\+39\s\d{3}\s\d{3}\s\d{4}$/.test(formData.phoneNumber)) {
            newErrors.phoneNumber = "Numero di telefono non è valido";
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };


    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!validate()) return;

        try {
            if (onSave) {
                await onSave(formData);
            }
        } catch (error) {
            console.error("Error saving contact:", error);
        }
    };

    return (
        <form className="p-fluid grid" onSubmit={handleSubmit}>
            <div className="field col-12 lg:col-6">
                <label htmlFor="firstName">Nome<span className="p-error">*</span> </label>
                <InputText invalid={errors.firstName} id="firstName" type="text" value={formData.firstName}
                           onChange={handleChange}/>
                {errors.firstName && <small className="p-error">{errors.firstName}</small>}
            </div>
            <div className="field col-12 lg:col-6">
                <label htmlFor="lastName">Cognome<span className="p-error">*</span></label>
                <InputText invalid={errors.lastName} id="lastName" type="text" value={formData.lastName}
                           onChange={handleChange}/>
                {errors.lastName && <small className="p-error">{errors.lastName}</small>}
            </div>
            <div className="field col-12 lg:col-6">
                <label htmlFor="gender">Sesso<span className="p-error">*</span></label>
                <Dropdown
                    id="gender"
                    value={formData.gender}
                    options={genderOptions}
                    onChange={handleGenderChange}
                    placeholder="Select Gender"
                    invalid={errors.gender}
                />
                {errors.gender && <small className="p-error">{errors.gender}</small>}
            </div>
            <div className="field col-12 lg:col-6">
                <label htmlFor="birthDate">Data di nascita</label>
                <Calendar id="birthDate" value={formData.birthDate} onChange={(e) => setFormData(prevData => ({
                    ...prevData,
                    birthDate: e.value
                }))} selectionMode='single'/>
            </div>
            <div className="field col-12 lg:col-6">
                <label htmlFor="phoneNumber">Numero di telefono</label>
                <InputMask
                    invalid={errors.phoneNumber}
                    value={formData.phoneNumber}
                    onChange={handlePhoneChange}
                    mask="+39 999 999 9999"
                    placeholder="+39 331 987 6543"
                />
                {errors.phoneNumber && <small className="p-error">{errors.phoneNumber}</small>}

            </div>
            <div className="field col-12 lg:col-6">
                <label htmlFor="email">Email<span className="p-error">*</span></label>
                <InputText invalid={errors.email} id="email" type="email" value={formData.email}
                           onChange={handleChange}/>
                {errors.email && <small className="p-error">{errors.email}</small>}
            </div>

            <div className="field col-12 lg:col-6">
                <label htmlFor="city">Città </label>
                <Dropdown id="city" value={formData.city} options={cities}
                          onChange={(e) => setFormData(prevData => ({
                              ...prevData,
                              city: e.value
                          }))} optionLabel="name" placeholder="Seleziona una città">
                </Dropdown>
            </div>

            <Button label="Save" icon="pi pi-check" type="submit"/>
        </form>
    );
};

export default ContactForm;
