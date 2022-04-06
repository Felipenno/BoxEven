import { Produto } from "./produto";

export class Localizacao {
  localizacaoId: number = -1;
  andar?: string;
  corredor?: number;
  lado?: string;
  prateleira?: number;
  vao?: number;
  produtoId?: number;
  produto: Produto = new Produto();
}