import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { Pedido } from 'src/app/shared/models/pedido';
import { PedidoAlterarStatus } from 'src/app/shared/models/pedido-alterar-status';
import { ResultadoModel } from 'src/app/shared/models/resultado-Model';

@Injectable({
  providedIn: 'root'
})
export class SeparacaoService {

  private  endPointUrl = 'pedido';

  constructor(private comunicacaoService: ComunicacaoService ) { }

  listarPedidos() : Observable<ResultadoModel>{
    return this.comunicacaoService.listar<ResultadoModel>(this.endPointUrl).pipe();
  }

  listarPedidosFiltro(paramsList?: ParamsModel[]) : Observable<ResultadoModel>{
    return this.comunicacaoService.listar<ResultadoModel>(this.endPointUrl + '/filtro', paramsList).pipe();
  }

  atualizarStatus(pedidoAlterarStatus: PedidoAlterarStatus): Observable<ResultadoModel>{
    return this.comunicacaoService.patch<PedidoAlterarStatus, ResultadoModel>(this.endPointUrl, pedidoAlterarStatus).pipe();
  }

}

