import React, { useEffect, useState } from 'react';
import { getProdukty } from '../services/apiService';

const ProduktList = () => {
    const [produkty, setProdukty] = useState([]);

    useEffect(() => {
        const fetchProdukty = async () => {
            try {
                const response = await getProdukty();
                setProdukty(response);
            } catch (error) {
                console.error('B³¹d podczas pobierania produktów:', error);
            }
        };

        fetchProdukty();
    }, []);

    return (
        <div>
            <h2>Lista produktów</h2>
            <ul>
                {produkty.map((produkt) => (
                    <li key={produkt.id}>{produkt.nazwa} - {produkt.cena} PLN</li>
                ))}
            </ul>
        </div>
    );
};

export default ProduktList;
