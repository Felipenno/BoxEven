import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';
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
    private produtoService: ProdutoService,
    private alertaService: AlertaService,
    private authService: AuthService
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
    this.movimentacao.usuarioId = this.authService.obterIdUsuario();

    this.movimentacaoService.criarMovimentacao(this.movimentacao).subscribe({
      next: res => { 
        if(res){
          if(res.sucesso){
            this.alertaService.sucesso(res.mensagem);
          }
          else{
            this.alertaService.erro(res.mensagem);
          }
        }
      }
    })
  }

  listarMovimentacoes(){
    this.movimentacaoService.listarMovimentacoes().subscribe({
      next: res => { 
        if(res && res.objeto){
          if(res.sucesso){
            this.movimentacoes = res.objeto;
          }
          else{
            this.alertaService.erro(res.mensagem)
          }
        }
      }
    })
  }

  procurarProduto(){
    let produtoId = this.movimentacaoForm.get('produtoId')?.value;
    if(produtoId){
      this.produtoService.listarProdutoPorId(produtoId).subscribe({
        next: res => { 
          if(res && res.objeto){
            if(res.sucesso){
              this.produto = res.objeto;
            }
            else{
              this.alertaService.erro(res.mensagem)
            }
          }
        }
      })  
    }
  }

}
