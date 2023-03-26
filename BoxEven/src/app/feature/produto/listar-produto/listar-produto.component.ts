import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';
import { PaginacaoService } from 'src/app/shared/components/paginacao/paginacao.service';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { Produto } from 'src/app/shared/models/produto';
import { ProdutoService } from '../produto.service';

@Component({
  selector: 'app-listar-produto',
  templateUrl: './listar-produto.component.html',
  styleUrls: ['./listar-produto.component.scss']
})
export class ListarProdutoComponent implements OnInit {

  produtos: Produto[] = [];
  filtroForm!: FormGroup;
  private paginaAtual: number = 1;
  readonly itensPagina = 10;

  readonly semImagem: string = './assets/img/no-img.png';
  
  constructor(
    private produtoService: ProdutoService,
    private fb: FormBuilder,
    private alertaService: AlertaService,
    private paginacaoService: PaginacaoService
  ) { }

  ngOnInit(): void {
    this.validacaoFiltro();
    this.paginacaoService.iniciarPaginacao();
    this.paginacaoService.obterPaginaAtual().subscribe(p => { 
      this.paginaAtual = p;
      this.listar();
    });

    //this.listar();
  }

  private validacaoFiltro(): void {
    this.filtroForm = this.fb.group({
      codigo: ['', [Validators.min(1)]],
      descricao: ['', [Validators.minLength(3), Validators.maxLength(30)]],
      status: ['']
    });
  }

  listar(): void {
    let parametros: ParamsModel[] = [];
    let codigo = this.filtroForm.get('codigo')?.value;
    let descricao = this.filtroForm.get('descricao')?.value;
    let status = this.filtroForm.get('status')?.value;

    parametros.push({ nome: 'itensPagina', valor: this.itensPagina })
    parametros.push({ nome: 'pagina', valor: this.paginaAtual })

    if (codigo) {
      parametros.push({ nome: 'id', valor: codigo })
    }

    if (descricao) {
      parametros.push({ nome: 'descricao', valor: descricao });
    }

    if (status === 'ativo') {
      parametros.push({ nome: 'status', valor: true })
    }
    else if (status === 'inativo') {
      parametros.push({ nome: 'status', valor: false })
    }

    this.produtoService.listarProdutos(parametros).subscribe({
      next: res => { 
        if(res && res.objeto){
          if(res.sucesso){
            this.produtos = res.objeto;
            this.paginacaoService.mudarTamanhoLista(this.produtos.length)
          }
          else{
            this.alertaService.erro(res.mensagem)
          }
        }
      }
    });

    
  }

}
