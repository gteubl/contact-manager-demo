'use client'

import {Card} from "primereact/card";
import {useRouter} from "next/navigation";
import {useEffect} from "react";
import {Button} from "primereact/button";
import {SecretApi} from "@/services/api";
import {OpenApiConfig} from "@/services/OpenApiConfig";


export default function Dashboard() {
    const router = useRouter();

    useEffect(() => {
        const token = document.cookie.split('; ').find(row => row.startsWith('authToken='));

        if (!token) {
            router.push('/login');
        }
    }, [router]);

    const handleSecret = async () => {
        const token = document.cookie.split('; ').find(row => row.startsWith('authToken='))?.split('=')[1];

        if (!token) {
            router.push('/login');
            return;
        }

        const response = await new SecretApi(OpenApiConfig.getConfig(token)).apiSecretSecretGet();
        alert(`Messagio del server: ${response}`);
    }

    const handleLogout = () => {
        document.cookie = 'authToken=; path=/; expires=Thu, 01 Jan 1970 00:00:00 UTC';
        router.push('/login');
    }

    return (
        <div className="p-fluid">
            <Card title="Scret Dashboard" className="gap-3">
                <div className="grid">
                    <div className="col-4">
                        <Button label="Logout" severity="warning" onClick={handleLogout}/>
                    </div>
                    <div className="col-8">
                        <Button label="Secret Buttom" severity="danger" onClick={handleSecret}/>
                    </div>
                </div>

            </Card>
        </div>
    );
}

