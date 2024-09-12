import { createBrowserRouter } from 'react-router-dom';

import Layout from './layout';
import { DashboardPage, UsersPage } from '@/pages';

const router = createBrowserRouter([
    {
        path: '/',
        Component: Layout,
        children: [
            {
                path: '/',
                Component: DashboardPage,
            },
            {
                path: 'users',
                Component: UsersPage,
            },
        ],
    },
]);

export default router;
