export interface CreateUserRequest {
    customerId: number,
    initialCredit: number,
    name: string | null,
    surname: string | null
}