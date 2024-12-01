import React from 'react';
import Header from './components/Header';
import Login from './components/Login';
import Register from './components/Register';
import ProduktList from './components/ProduktList';
import Koszyk from './components/Koszyk';

function App() {
    return (
        <div className="App">
            <Header />
            <Login />
            <Register />
            <ProduktList />
            <Koszyk />
        </div>
    );
}

export default App;
