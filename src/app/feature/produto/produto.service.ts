import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { Produto } from 'src/app/shared/models/produto';
import { ProdutoEditar } from 'src/app/shared/models/produto-editar';
import { ProdutoNovo } from 'src/app/shared/models/produto-novo';

@Injectable()
export class ProdutoService {

  private  endPointUrl = 'produto';

  constructor(private comunicacaoService: ComunicacaoService ) { }

  listarProdutos(paramsList?: ParamsModel[]) : Observable<Produto[]>{
    return this.comunicacaoService.listar<Produto[]>(this.endPointUrl, paramsList).pipe();
  }

  listarDesalocados() : Observable<Produto[]>{
    return this.comunicacaoService.listar<Produto[]>(this.endPointUrl + '/desalocado').pipe();
  }

  listarProdutoPorId(produtoId: number) : Observable<Produto>{
    return this.comunicacaoService.listarPorId<Produto>(this.endPointUrl, produtoId).pipe();
  }

  cadastrarProduto(produto: ProdutoNovo): Observable<ProdutoNovo>{
    return this.comunicacaoService.salvar<ProdutoNovo>(this.endPointUrl, produto).pipe();
  }

  atualizarProduto(produto: ProdutoEditar, produtoId: number){
    return this.comunicacaoService.editar<ProdutoEditar>(this.endPointUrl, produtoId, produto).pipe();
  }
}
