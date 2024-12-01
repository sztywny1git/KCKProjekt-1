import React, { useState } from 'react';
import { zarejestruj } from '../services/apiService';

const Register = () => {
    const [nazwa, setNazwa] = useState('');
    const [haslo, setHaslo] = useState('');
    const [message, setMessage] = useState('');

    const handleRegister = async () => {
        try {
            const response = await zarejestruj(nazwa, haslo);
            setMessage(response.message);
        } catch (error) {
            setMessage('Wyst¹pi³ b³¹d podczas rejestracji.');
        }
    };

    return (
        <div>
            <h2>Rejestracja</h2>
            <input
                type="text"
                placeholder="Nazwa u¿ytkownika"
                value={nazwa}
                onChange={(e) => setNazwa(e.target.value)}
            />
            <input
                type="password"
                placeholder="Has³o"
                value={haslo}
                onChange={(e) => setHaslo(e.target.value)}
            />
            <button onClick={handleRegister}>Zarejestruj</button>
            <p>{message}</p>
        </div>
    );
};

export default Register;
