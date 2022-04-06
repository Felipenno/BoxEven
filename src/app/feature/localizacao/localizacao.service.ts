import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { Localizacao } from 'src/app/shared/models/localizacao';
import { LocalizacaoEndereco } from 'src/app/shared/models/localizacao-endereco';
import { ParamsModel } from 'src/app/shared/models/params-model';

@Injectable({
  providedIn: 'root'
})
export class LocalizacaoService {

  private  endPointUrl = 'localizacao';

  constructor(private comunicacaoService: ComunicacaoService) { }

  listarLocalizacoes(paramsList?: ParamsModel[]): Observable<Localizacao[]>{
    return this.comunicacaoService.listar<Localizacao[]>(this.endPointUrl, paramsList).pipe();
  }

  listarLocalizacaoPorId(routerId: number): Observable<Localizacao>{
    return this.comunicacaoService.listarPorId<Localizacao>(this.endPointUrl, routerId).pipe();
  }

  listarLocalizacoesDisponiveis() : Observable<LocalizacaoEndereco[]>{
    return this.comunicacaoService.listar<LocalizacaoEndereco[]>(this.endPointUrl + '/LocalizacoesDisponiveis').pipe();
  }

  VerificarEnderecoDisponivel(){

  }

  criarLocalizacao(localizacao: Localizacao): Observable<Localizacao>{
    return this.comunicacaoService.salvar<Localizacao>(this.endPointUrl, localizacao).pipe();
  }

  editarLocalizacao(localizacao: Localizacao, localizacaoId: number){
    return this.comunicacaoService.editar<Localizacao>(this.endPointUrl, localizacaoId, localizacao).pipe();
  }

  removerLocalizacao(localizacaoId: number){
    return this.comunicacaoService.deletar(this.endPointUrl, localizacaoId).pipe();
  }
}
