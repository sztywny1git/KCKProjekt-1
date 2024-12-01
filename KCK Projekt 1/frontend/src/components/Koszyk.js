import React, { useEffect, useState } from 'react';
import { api } from '../services/apiService';

const ProduktList = () => {
    const [produkty, setProdukty] = useState([]);

    useEffect(() => {
        const fetchProdukty = async () => {
            try {
                const response = await api.getProdukty();
                setProdukty(response);
            } catch (error) {
                console.error('B��d podczas pobierania produkt�w:', error);
            }
        };
        fetchProdukty();
    }, []);

    return (
        <div>
            <h2>Lista produkt�w</h2>
            <ul>
                {produkty.map((produkt) => (
                    <li key={produkt.id}>{produkt.nazwa} - {produkt.cena} PLN</li>
                ))}
            </ul>
        </div>
    );
};

export default ProduktList;
