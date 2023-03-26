export class ResultadoModel {
  sucesso: boolean;
  mensagem: string;
  objeto: any;

  constructor(){
    this.sucesso = false;
    this.mensagem = '';
  }
}