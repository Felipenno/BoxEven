import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { Produto } from 'src/app/shared/models/produto';
import { ProdutoEditar } from 'src/app/shared/models/produto-editar';
import { ProdutoNovo } from 'src/app/shared/models/produto-novo';
import { ResultadoModel } from 'src/app/shared/models/resultado-Model';

@Injectable()
export class ProdutoService {

  private  endPointUrl = 'produto';

  constructor(private comunicacaoService: ComunicacaoService ) { }

  listarProdutos(paramsList?: ParamsModel[]) : Observable<ResultadoModel>{
    return this.comunicacaoService.listar<ResultadoModel>(this.endPointUrl, paramsList).pipe();
  }

  listarDesalocados() : Observable<ResultadoModel>{
    return this.comunicacaoService.listar<ResultadoModel>(this.endPointUrl + '/desalocado').pipe();
  }

  listarProdutoPorId(produtoId: number) : Observable<ResultadoModel>{
    return this.comunicacaoService.listarPorId<ResultadoModel>(this.endPointUrl, produtoId).pipe();
  }

  cadastrarProduto(produto: ProdutoNovo): Observable<ResultadoModel>{
    return this.comunicacaoService.salvar<ProdutoNovo, ResultadoModel>(this.endPointUrl, produto).pipe();
  }

  atualizarProduto(produto: ProdutoEditar, produtoId: number) : Observable<ResultadoModel>{
    return this.comunicacaoService.editar<ProdutoEditar, ResultadoModel>(this.endPointUrl, produtoId, produto).pipe();
  }

}
