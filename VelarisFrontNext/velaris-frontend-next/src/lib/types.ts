export interface TodoItem {
    id: number;
    title: string;
    isCompleted: boolean;
    userId: string;
    dueDate: string;
}

export interface User {
    id: number;
    username: string;
    email: string;
    createdAt: string;
}

export interface RegisterRequest {
    username: string;
    password: string;
    email: string;
}

export interface AddTodoRequest {
    title: string;
    dueDate: string;
}

export interface EditTodoRequest {
    id: number;
    title: string;
    isCompleted: boolean;
    dueDate: string;
}