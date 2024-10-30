'use client'

import {Button} from "primereact/button";
import {useRouter} from 'next/navigation';
import {Toolbar} from "primereact/toolbar";


const NavBar = () => {

    const router = useRouter();

    const handleHome = (e) => {
        e.preventDefault();
        router.push('/');
    }

    const handleGoDashboard = (e) => {
        e.preventDefault();
        router.push('/dashboard');
    }

    const startContent = (
        <Button rounded outlined icon="pi pi-home" onClick={handleHome}/>
    );

    const endContent = (
        <Button rounded outlined icon="pi pi-microsoft" onClick={handleGoDashboard}/>
    );

    return (
        <Toolbar start={startContent}
                 end={endContent}
        ></Toolbar>
    );
}

export default NavBar;





