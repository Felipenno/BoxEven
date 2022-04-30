import { Produto } from "./produto";

export class Pedido {
    id: string = '';
    numero: number = 0;
    vendedor: string = '';
    status: number = 0;
    criacao?: Date ;
    conclusao?: Date;
    produtos: Produto[] = []
}