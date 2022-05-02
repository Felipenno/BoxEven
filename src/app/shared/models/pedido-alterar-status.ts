import { ProdutoAlterarQuantidade } from "./produto-alterar-quantidade";

export class PedidoAlterarStatus {
    idPedido: string = '';
    statusPedido: number = 0;
    idUsuario?: string;
    numeroPedido?: number;
    produtos?: ProdutoAlterarQuantidade[];
}