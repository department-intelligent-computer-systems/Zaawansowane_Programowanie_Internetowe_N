import { Home } from "./components/Home";
import { AddressGrid } from './components/address/AddressGrid';
import { AddressAdd } from "./components/address/AddressAdd";
import { AddressEdit } from './components/address/AddressEdit';
import { AddressDelete } from "./components/address/AddressDelete";
import React from "react";
import { CategoryList } from "./components/category/CategoryList";
import { CategoryProvider } from './contexts/CategoryContext';
const AppRoutes = [
    {
        index: true,
        element:  <Home />
    },
    {
        path: '/home',
        element: <Home />
    },
    {
        path: '/address',
        element: <AddressGrid />
    },
    {
        path: '/address/add',
        element: <AddressAdd />
    },
    {
        path: '/address/edit/:id',
        element: <AddressEdit />
    },
    {
        path: '/address/delete/:id',
        element: <AddressDelete />
    },
    {
        path: '/category',
        element: <CategoryProvider>
                    <CategoryList/>
                </CategoryProvider>
    }
];
export default AppRoutes;