import { LocalizacaoEndereco } from "./localizacao-endereco";
import { UnidadeMedida } from "./unidade-medida";

export class Produto {
  produtoId: number = 0;
  ativo?: boolean;
  quantidade?: number;
  preco?: number;
  nome?: string;
  imagem?: File;
  codigoBarras?: string;
  criacao?: Date;
  atualizacao?: Date;
  unidadeMedida: UnidadeMedida = new UnidadeMedida();
  localizacoes?: LocalizacaoEndereco[];
}