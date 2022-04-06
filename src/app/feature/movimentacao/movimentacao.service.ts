import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { MovimentacaoCriar } from 'src/app/shared/models/movimentacao-criar';
import { MovimentacaoListar } from 'src/app/shared/models/movimentacao-listar';

@Injectable({
  providedIn: 'root'
})
export class MovimentacaoService {

  private readonly endPointUrl = 'movimentacao';

  constructor(private comunicacaoService: ComunicacaoService) { }

  listarMovimentacoes(): Observable<MovimentacaoListar[]>{
    return this.comunicacaoService.listar<MovimentacaoListar[]>(this.endPointUrl).pipe();
  }

  criarMovimentacao(movimentacao: MovimentacaoCriar): Observable<any>{
    return this.comunicacaoService.salvar<MovimentacaoCriar>(this.endPointUrl, movimentacao).pipe()
  }
}