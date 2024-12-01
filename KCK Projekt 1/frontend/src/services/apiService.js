import axios, { AxiosInstance, AxiosResponse } from 'axios';

const API_URL = 'http://localhost:5000/api'; // Zmieñ na adres swojego backendu

export const getProdukty = async () => {
    try {
        const response = await axios.get(`${API_URL}/produkty`);
        return response.data;
    } catch (error) {
        console.error('B³¹d podczas pobierania produktów:', error);
        throw error;
    }
};

export const dodajDoKoszyka = async (produktId, ilosc) => {
    try {
        const response = await axios.post(`${API_URL}/koszyk/dodaj`, { produktId, ilosc });
        return response.data;
    } catch (error) {
        console.error('B³¹d podczas dodawania produktu do koszyka:', error);
        throw error;
    }
};

export const pobierzKoszyk = async () => {
    try {
        const response = await axios.get(`${API_URL}/koszyk`);
        return response.data;
    } catch (error) {
        console.error('B³¹d podczas pobierania koszyka:', error);
        throw error;
    }
};

export const zaloguj = async (nazwa, haslo) => {
    try {
        const response = await axios.post(`${API_URL}/uzytkownik/logowanie`, { nazwa, haslo });
        return response.data;
    } catch (error) {
        console.error('B³¹d podczas logowania:', error);
        throw error;
    }
};

export const zarejestruj = async (nazwa, haslo) => {
    try {
        const response = await axios.post(`${API_URL}/uzytkownik/rejestracja`, { nazwa, haslo });
        return response.data;
    } catch (error) {
        console.error('B³¹d podczas rejestracji:', error);
        throw error;
    }
};
