'use client'

import {Button} from "primereact/button";
import {useRouter} from 'next/navigation';
import {Menubar} from "primereact/menubar";


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
        <Menubar start={startContent}
                 end={endContent}
        ></Menubar>
    );
}

export default NavBar;





