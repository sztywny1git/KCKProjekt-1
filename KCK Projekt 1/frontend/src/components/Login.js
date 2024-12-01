import React, { useState } from 'react';
import { zaloguj } from '../services/apiService';

const Login = () => {
    const [nazwa, setNazwa] = useState('');
    const [haslo, setHaslo] = useState('');
    const [message, setMessage] = useState('');

    const handleLogin = async () => {
        try {
            const response = await zaloguj(nazwa, haslo);
            setMessage(`Zalogowano pomyœlnie! Status admina: ${response.isAdmin}`);
        } catch (error) {
            setMessage('B³êdna nazwa u¿ytkownika lub has³o.');
        }
    };

    return (
        <div>
            <h2>Logowanie</h2>
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
            <button onClick={handleLogin}>Zaloguj</button>
            <p>{message}</p>
        </div>
    );
};

export default Login;
