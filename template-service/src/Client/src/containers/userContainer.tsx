import { useState, memo, useEffect } from 'react';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { PlusCircle, RefreshCw } from 'lucide-react';
interface IUser {
    id: number;
    name: string;
}
import { useApi } from '@/hooks';

const UserContainer = () => {
    const [users, setUsers] = useState<IUser[]>([]);
    const [newUserName, setNewUserName] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const { fetch: getUsersRequest } = useApi('/');
    const { fetch: addUserRequest } = useApi('/', 'POST');

    const setUsersFromRequest = async () => {
        const newUsers = (await getUsersRequest()) as IUser[];
        setUsers(newUsers);
    };

    useEffect(() => {
        setUsersFromRequest();
    }, []);

    const addUser = async () => {
        if (newUserName.trim() === '') return;

        const newUser = {
            id: users.length + 1,
            name: newUserName,
        };
        if (users.length) {
            setUsers([...users, newUser]);
        } else {
            setUsers([newUser]);
        }
        addUserRequest({ name: newUserName });
        setNewUserName('');
    };

    const fetchUsers = async () => {
        try {
            setIsLoading(true);
            await setUsersFromRequest();
        } catch (error) {
            console.error('Error:', error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <>
            <h1 className="text-3xl font-bold mb-6">Users</h1>

            <div className="mb-6 flex items-center space-x-4">
                <Input
                    type="text"
                    placeholder="Enter user name"
                    value={newUserName}
                    onChange={e => setNewUserName(e.target.value)}
                    className="max-w-xs"
                />
                <Button onClick={addUser} className="flex items-center">
                    <PlusCircle className="mr-2 h-4 w-4" />
                    Add User
                </Button>
                <Button
                    onClick={fetchUsers}
                    variant="outline"
                    className="flex items-center"
                    disabled={isLoading}
                >
                    <RefreshCw className={`mr-2 h-4 w-4 ${isLoading ? 'animate-spin' : ''}`} />
                    {isLoading ? 'Fetching...' : 'Fetch Users'}
                </Button>
            </div>

            <div className="bg-white shadow-md rounded-lg overflow-hidden">
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead>Name</TableHead>
                            <TableHead>Id</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {users?.map(user => (
                            <TableRow key={user.id}>
                                <TableCell>{user.id}</TableCell>
                                <TableCell>{user.name}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </div>
        </>
    );
};

export default memo(UserContainer);
