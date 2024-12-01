import React, { useState } from 'react';
import { api } from '../services/apiService';

const Login = () => {
    const [nazwa, setNazwa] = useState('');
    const [haslo, setHaslo] = useState('');
    const [message, setMessage] = useState('');

    const handleLogin = async () => {
        try {
            const response = await api.zaloguj(nazwa, haslo);
            setMessage(`Zalogowano pomy�lnie! Status admina: ${response.isAdmin}`);
        } catch (error) {
            setMessage('B��dna nazwa u�ytkownika lub has�o.');
        }
    };

    return (
        <div>
            <h2>Logowanie</h2>
            <input
                type="text"
                placeholder="Nazwa u�ytkownika"
                value={nazwa}
                onChange={(e) => setNazwa(e.target.value)}
            />
            <input
                type="password"
                placeholder="Has�o"
                value={haslo}
                onChange={(e) => setHaslo(e.target.value)}
            />
            <button onClick={handleLogin}>Zaloguj</button>
            <p>{message}</p>
        </div>
    );
};

export default Login;
