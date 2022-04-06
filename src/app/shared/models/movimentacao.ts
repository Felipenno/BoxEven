import { Produto } from "./produto";
import { Usuario } from "./usuario";

export class Movimentacao {
  movimentacaoId?: number;
  quantidade?: number;
  Justificativa?: string;
  dataOperacao?: Date;
  tipo?: string;
  produto?: Produto;
  usuario?: Usuario;
}