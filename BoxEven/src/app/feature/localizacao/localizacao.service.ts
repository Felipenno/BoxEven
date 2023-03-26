import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { Localizacao } from 'src/app/shared/models/localizacao';
import { LocalizacaoEndereco } from 'src/app/shared/models/localizacao-endereco';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { ResultadoModel } from 'src/app/shared/models/resultado-Model';

@Injectable({
  providedIn: 'root'
})
export class LocalizacaoService {

  private  endPointUrl = 'localizacao';

  constructor(private comunicacaoService: ComunicacaoService) { }

  listarLocalizacoes(paramsList?: ParamsModel[]): Observable<ResultadoModel>{
    return this.comunicacaoService.listar<ResultadoModel>(this.endPointUrl, paramsList).pipe();
  }

  listarLocalizacaoPorId(routerId: number): Observable<ResultadoModel>{
    return this.comunicacaoService.listarPorId<ResultadoModel>(this.endPointUrl, routerId).pipe();
  }

  listarLocalizacoesDisponiveis() : Observable<ResultadoModel>{
    return this.comunicacaoService.listar<ResultadoModel>(this.endPointUrl + '/LocalizacoesDisponiveis').pipe();
  }

  VerificarEnderecoDisponivel(){

  }

  criarLocalizacao(localizacao: Localizacao): Observable<ResultadoModel>{
    return this.comunicacaoService.salvar<Localizacao, ResultadoModel>(this.endPointUrl, localizacao).pipe();
  }

  editarLocalizacao(localizacao: Localizacao, localizacaoId: number): Observable<ResultadoModel>{
    return this.comunicacaoService.editar<Localizacao, ResultadoModel>(this.endPointUrl, localizacaoId, localizacao).pipe();
  }

  removerLocalizacao(localizacaoId: number): Observable<ResultadoModel>{
    return this.comunicacaoService.deletar(this.endPointUrl, localizacaoId).pipe();
  }
}
