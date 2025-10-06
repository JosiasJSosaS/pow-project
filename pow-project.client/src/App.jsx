import { useState } from 'react';

import './App.css';
import Header from './components/Header';

function App() {
    const [currentPage, setCurrentPage] = useState({
        title: 'Listas',
        name: 'listas'
    });

    function handleChangePage() {
        setCurrentPage({
            title: 'Listas',
            name: 'listas'
        });
    }

    

    let content;
    switch (currentPage.name) {
        case 'listas': {
            content = <h1> Contenido </h1>;
            break;
        }

        default: {
            content = null;
        }
    }

    return (
        <>
            <Header title={currentPage.title} />
            { content }
        </>
    )
}

export default App;