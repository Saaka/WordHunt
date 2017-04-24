export class UserModel {
    id: number;
    name: string;
    email: string;
    loggedIn: boolean;
    tokenExpirationDate: Date;
    admin: boolean;
}