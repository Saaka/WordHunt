export class UserModel {
    name: string;
    email: string;
    loggedIn: boolean;
    tokenExpirationDate: Date;
    admin: boolean;
}