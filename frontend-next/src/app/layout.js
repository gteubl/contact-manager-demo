import localFont from "next/font/local";
import "./globals.css";
import PrimeReactStyles from "@/components/shared/PrimeReactStyles";

const geistSans = localFont({
    src: "./fonts/GeistVF.woff",
    variable: "--font-geist-sans",
    weight: "100 900",
});
const geistMono = localFont({
    src: "./fonts/GeistMonoVF.woff",
    variable: "--font-geist-mono",
    weight: "100 900",
});

export const metadata = {
    title: "Contact Manager Demo",
    description: "Una rubrica di contatti",
};

export default function RootLayout({children}) {
    return (
        <html lang="it">
        <body
            className={`${geistSans.variable} ${geistMono.variable} antialiased`}
        >
        <PrimeReactStyles/>
        {children}
        </body>
        </html>
    );
}
