import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovimentacaoCriar } from 'src/app/shared/models/movimentacao-criar';
import { MovimentacaoListar } from 'src/app/shared/models/movimentacao-listar';
import { Produto } from 'src/app/shared/models/produto';
import { ProdutoService } from '../../produto/produto.service';
import { MovimentacaoService } from '../movimentacao.service';

@Component({
  selector: 'app-criar-movimentacao',
  templateUrl: './criar-movimentacao.component.html',
  styleUrls: ['./criar-movimentacao.component.scss']
})
export class CriarMovimentacaoComponent implements OnInit {
  movimentacao!: MovimentacaoCriar;
  movimentacoes: MovimentacaoListar[] = [] ;
  movimentacaoForm!: FormGroup;

  produto!: Produto;

  readonly semImagem: string = './assets/img/no-img.png';

  constructor(
    private fb: FormBuilder,
    private movimentacaoService: MovimentacaoService,
    private produtoService: ProdutoService
  ) { }

  ngOnInit(): void {
    this.validacaoForm();
    this.listarMovimentacoes();
  }

  private validacaoForm(): void {
    this.movimentacaoForm = this.fb.group({
      quantidade: ['', [Validators.required, Validators.min(1)]],
      justificativa: ['', [Validators.required]],
      tipo: ['', [Validators.required]],
      produtoId: ['', [Validators.required, Validators.min(1)]],
    });
  }

  criarMovimentacao(){
    this.movimentacao = this.movimentacaoForm.value;
    this.movimentacao.usuarioId = "0f8fad5b-d9cb-469f-a165-70867728950e";

    this.movimentacaoService.criarMovimentacao(this.movimentacao).subscribe({
      next: dados => console.log(dados),
      error: err => console.log(err)
    })
  }

  listarMovimentacoes(){
    this.movimentacaoService.listarMovimentacoes().subscribe({
      next: dados => this.movimentacoes = dados,
      error: err => console.log(err)
    })
  }

  procurarProduto(){
    let produtoId = this.movimentacaoForm.get('produtoId')?.value;
    if(produtoId){
      this.produtoService.listarProdutoPorId(produtoId).subscribe({
        next: dados => this.produto = dados,
        error: err => console.log(err)
      })
  
      console.log(this.produto)
    }
  }

}
