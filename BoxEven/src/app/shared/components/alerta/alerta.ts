export class Alerta {
    id: string;
    tipo: TipoAlerta;
    mensagem: string;
    fecharAutomatico: boolean;
    tempoParaFechar: number;
    botaoFechar: boolean

    constructor(init?:Alerta) {
        this.id = '';
        this.tipo = TipoAlerta.Aviso;
        this.mensagem = '';
        this.fecharAutomatico = true;
        this.tempoParaFechar = 6000;
        this.botaoFechar = true;

        Object.assign(this, init);
    }
}

export enum TipoAlerta {
    Sucesso,
    Erro,
    Info,
    Aviso
}