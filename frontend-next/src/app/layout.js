import "./globals.css";
import NavBar from "@/components/NavBar";

export const metadata = {
    title: "Contact Manager Demo",
    description: "Una rubrica di contatti",
};

export default function RootLayout({children}) {
    return (
        <html lang="it">
        <body className="antialiased">
        <NavBar/>
        {children}
        </body>
        </html>
    );
}
