import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { MovimentacaoCriar } from 'src/app/shared/models/movimentacao-criar';
import { ResultadoModel } from 'src/app/shared/models/resultado-Model';

@Injectable({
  providedIn: 'root'
})
export class MovimentacaoService {

  private readonly endPointUrl = 'movimentacao';

  constructor(private comunicacaoService: ComunicacaoService) { }

  listarMovimentacoes(): Observable<ResultadoModel>{
    return this.comunicacaoService.listar<ResultadoModel>(this.endPointUrl).pipe();
  }

  criarMovimentacao(movimentacao: MovimentacaoCriar): Observable<ResultadoModel>{
    return this.comunicacaoService.salvar<MovimentacaoCriar, ResultadoModel>(this.endPointUrl, movimentacao).pipe()
  }

}