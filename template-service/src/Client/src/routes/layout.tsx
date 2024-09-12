import { memo } from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';
import { LayoutDashboard, LogOut, Users } from 'lucide-react';

const menuItems = [
    { name: 'Dashboard', href: '/', icon: LayoutDashboard },
    { name: 'Users', href: '/users', icon: Users },
];

export const Layout = () => {
    const { pathname } = useLocation();

    return (
        <div className="flex h-screen bg-gray-100">
            <aside className="w-64 bg-white shadow-md">
                <div className="p-4">
                    <h1 className="text-2xl font-bold text-gray-800">Admin Panel</h1>
                </div>
                <nav className="mt-4">
                    <ul>
                        {menuItems.map(item => (
                            <li key={item.name}>
                                <Link
                                    to={item.href}
                                    className={`flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200 ${
                                        pathname === item.href ? 'bg-gray-200' : ''
                                    }`}
                                >
                                    <item.icon className="w-5 h-5 mr-2" />
                                    {item.name}
                                </Link>
                            </li>
                        ))}
                    </ul>
                </nav>
                <div className="absolute bottom-0 w-64 p-4">
                    <button className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200 w-full">
                        <LogOut className="w-5 h-5 mr-2" />
                        Logout
                    </button>
                </div>
            </aside>

            <main className="flex-1 p-8 overflow-y-auto">
                <Outlet />
            </main>
        </div>
    );
};
export default memo(Layout);
