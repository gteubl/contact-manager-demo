'use client'

import {useRouter} from "next/navigation";
import {Button} from "primereact/button";
import {AuthorizationApi, ContactsApi} from "@/services/api";
import {OpenApiConfig} from "@/services/OpenApiConfig";
import {Card} from "primereact/card";
import {InputText} from "primereact/inputtext";
import {useState} from "react";

export default function Page() {

    const router = useRouter();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleLogin = async () => {
        const requestParameters = {username, password};

        try {
            const response = await new AuthorizationApi(OpenApiConfig.getConfig()).apiAuthTokenPost(requestParameters);

            if (response.accessToken) {
                document.cookie = `authToken=${response.accessToken}; path=/; expires=${new Date(response.expiresIn).toUTCString()}`;
                router.push('/dashboard');
            }
        } catch (error) {
            if (error.response?.status === 401) {
                setError("Credenziali non valide. Riprova.");
            } else {
                setError("Si è verificato un errore imprevisto. Riprova più tardi.");
            }
        }
    };


    return (
        <div className="flex justify-content-center mt-6">
            <Card title="Login">
                <form className="p-fluid" onSubmit={(e) => e.preventDefault()}>
                    <div className="field">
                        <label htmlFor="username">Username</label>
                        <InputText id="username" type="text" value={username}
                                   onChange={(e) => setUsername(e.target.value)}/>
                    </div>

                    <div className="field">
                        <label htmlFor="password">Password</label>
                        <InputText id="password" type="password" value={password}
                                   onChange={(e) => setPassword(e.target.value)}/>

                        {error && <small className="p-error ">{error}</small>}
                    </div>
                    <Button onClick={handleLogin} label="Login"></Button>
                </form>
            </Card>
        </div>
    )
}
