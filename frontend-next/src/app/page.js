import ContactTable from "@/components/ContactTable";
import {Card} from "primereact/card";

export default function Home() {
    return (
        <div className="p-fluid">
            <Card title="Rubrica contatti" className="gap-3">
                <ContactTable/>
            </Card>
        </div>
    );
}
