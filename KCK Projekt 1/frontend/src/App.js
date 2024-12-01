import React from 'react';
import Header from './components/Header';
import Login from './components/Login';
import Register from './components/Register';
import ProduktList from './components/ProduktList';
import Koszyk from './components/Koszyk';
import { AuthProvider } from './AuthContext';

function App() {
    return (
        <AuthProvider>
        <div className="App">
            <Header />
            <Login />
            <Register />
            <ProduktList />
            <Koszyk />
        </div>
        </AuthProvider>
    );
}

export default App;
