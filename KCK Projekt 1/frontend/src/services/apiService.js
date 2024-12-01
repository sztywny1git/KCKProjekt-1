import axios, { AxiosInstance, AxiosResponse } from 'axios';

const API_URL = 'http://localhost:5000/api/uzytkownik/'; // Zmień na adres swojego backendu

// Tworzenie instancji axios z predefiniowanym URL-em i nagłówkami
const axiosInstance = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const api = {
  getProdukty: async () => {
    try {
      const response: AxiosResponse = await axiosInstance.get(`/produkty`);
      return response.data;
    } catch (error) {
      console.error('Błąd podczas pobierania produktów:', error);
      throw error;
    }
  },

  dodajDoKoszyka: async (produktId: number, ilosc: number) => {
    try {
      const response: AxiosResponse = await axiosInstance.post(`/koszyk/dodaj`, { produktId, ilosc });
      return response.data;
    } catch (error) {
      console.error('Błąd podczas dodawania produktu do koszyka:', error);
      throw error;
    }
  },

  pobierzKoszyk: async () => {
    try {
      const response: AxiosResponse = await axiosInstance.get(`/koszyk`);
      return response.data;
    } catch (error) {
      console.error('Błąd podczas pobierania koszyka:', error);
      throw error;
    }
  },

  zaloguj: async (nazwa: string, haslo: string) => {
    try {
      const response: AxiosResponse = await axiosInstance.post(`/logowanie`, { nazwa, haslo });
      return response.data;
    } catch (error) {
      console.error('Błąd podczas logowania:', error);
      throw error;
    }
  },

  zarejestruj: async (nazwa: string, haslo: string) => {
    try {
      const response: AxiosResponse = await axiosInstance.post(`/rejestracja`, { nazwa, haslo });
      return response.data;
    } catch (error) {
      console.error('Błąd podczas rejestracji:', error);
      throw error;
    }
  },
};
