import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';
import { Localizacao } from 'src/app/shared/models/localizacao';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { Produto } from 'src/app/shared/models/produto';
import { ProdutoService } from '../../produto/produto.service';
import { LocalizacaoService } from '../localizacao.service';

@Component({
  selector: 'app-listar-localizacao',
  templateUrl: './listar-localizacao.component.html',
  styleUrls: ['./listar-localizacao.component.scss']
})
export class ListarLocalizacaoComponent implements OnInit {
  readonly semImagem: string = './assets/img/no-img.png';
  localizacoes: Localizacao[] = [];
  produtos: Produto[] = [];
  produtoAlocado: any;
  localizacaoForm!: FormGroup;
  filtroForm!: FormGroup;
  tipoRequisicao!: string;
  editarId!: number;
  removerId!: number;

  constructor(
    private localizacaoService: LocalizacaoService,
    private produtoService: ProdutoService,
    private fb: FormBuilder,
    private alertaService: AlertaService
  ) { }

  ngOnInit(): void {
    this.validacaoFiltro();
    this.validacaoLocalizacao();
    this.listarLocalizacoes();
  }

  private validacaoFiltro(): void {
    this.filtroForm = this.fb.group({
      andar: [''],
      corredor: [''],
      lado: [''],
      prateleira: [''],
      produtoCodigo: ['', [Validators.min(1), Validators.max(999999999)]],
    });
  }

  private validacaoLocalizacao(): void {
    this.localizacaoForm = this.fb.group({
      andar: ['', [Validators.required]],
      corredor: ['', [Validators.required]],
      lado: ['', [Validators.required]],
      prateleira: ['', [Validators.required]],
      vao: ['', [Validators.required]]
    });
  }

  gerarNumeros(limite: number): number[]{
    let numeros: number[] = [];

    for (let index = 1; index <= limite; index++) {
      numeros.push(index);
    }

    return numeros;
  }

  abrirModal(elementoId: string){
    let modal = document.getElementById(elementoId)
    modal!.style.display = "block";
  }

  fecharModal(elementoId: string){
    let modal = document.getElementById(elementoId)
    modal!.style.display = "none";
  }

  listarLocalizacoes():void{
    let parametros: ParamsModel[] = [];
    let filtro = this.filtroForm.value;

    if(filtro.andar){
      parametros.push({ nome: 'Andar', valor: filtro.andar })
    }

    if(filtro.corredor){
      parametros.push({ nome: 'Corredor', valor: filtro.corredor })
    }

    if(filtro.lado){
      parametros.push({ nome: 'Lado', valor: filtro.lado })
    }

    if(filtro.prateleira){
      parametros.push({ nome: 'Prateleira', valor: filtro.prateleira })
    }

    if(filtro.produtoCodigo){
      parametros.push({ nome: 'ProdutoId', valor: filtro.produtoCodigo })
    }

    this.localizacaoService.listarLocalizacoes(parametros).subscribe({
      next: res => { 
        if(res && res.objeto){
          if(res.sucesso){
            this.localizacoes = res.objeto;
          }
          else{
            this.alertaService.erro(res.mensagem)
          }
        }
      }
    })
  }

  listarProdutosDesalocados(){
    this.produtoService.listarDesalocados().subscribe({
      next: res => { 
        if(res && res.objeto){
          if(res.sucesso){
            this.produtos = res.objeto;
          }
          else{
            this.alertaService.erro(res.mensagem)
          }
        }
      }
    })
  }

  removerProduto(){
    this.produtoAlocado = undefined;
  }

  alocarProduto(id: number){
    this.produtoAlocado = this.produtos.find(x => x.produtoId === id)
  }

  criarLocalizacao(){
    this.localizacaoForm.reset({andar: '', corredor: '', lado: '', prateleira: '', vao: ''});
    this.removerProduto();
    this.abrirModal('localizacaoModal');
    this.listarProdutosDesalocados();
    this.tipoRequisicao = 'POST'

  }

  editarLocalizacao(localizacaoId: number){
    this.localizacaoForm.reset();
    this.abrirModal('localizacaoModal');
    this.listarProdutosDesalocados();
    this.tipoRequisicao = 'PUT'
    this.editarId = localizacaoId;

    this.localizacaoService.listarLocalizacaoPorId(localizacaoId).subscribe({
      next: res => { 
        if(res && res.objeto){
          if(res.sucesso){
            this.localizacaoForm.patchValue(res.objeto);
            this.produtoAlocado = res.objeto.produto;
          }
          else{
            this.alertaService.erro(res.mensagem)
          }
        }
      }
    })
  }

  removerLocalizacao(localizacaoId: number){
    this.abrirModal('deletarLocalizacaoModal');
    this.tipoRequisicao = 'DELETE'
    this.removerId = localizacaoId;
  }

  salvarAlteracao(){
    let requisicao = this.tipoRequisicao;

    switch (requisicao) {
      case 'POST':
          let formCriar = this.localizacaoForm.value;
          let localizacaoCriar: Localizacao = new Localizacao();
          localizacaoCriar.andar = formCriar.andar;
          localizacaoCriar.corredor = formCriar.corredor;
          localizacaoCriar.lado = formCriar.lado;
          localizacaoCriar.prateleira = formCriar.prateleira;
          localizacaoCriar.vao = formCriar.vao;

          if(this.produtoAlocado){
            localizacaoCriar.produtoId = this.produtoAlocado.produtoId
          }
              
          this.localizacaoService.criarLocalizacao(localizacaoCriar).subscribe({
            next: res => { 
              if(res){
                if(res.sucesso){
                  this.alertaService.sucesso(res.mensagem)
                }
                else{
                  this.alertaService.erro(res.mensagem)
                }
              }
            }
          })
        break;
      case 'PUT':
          let formEditar = this.localizacaoForm.value;
          let localizacaoEditar: Localizacao = new Localizacao();
          localizacaoEditar.andar = formEditar.andar;
          localizacaoEditar.corredor = formEditar.corredor;
          localizacaoEditar.lado = formEditar.lado;
          localizacaoEditar.prateleira = formEditar.prateleira;
          localizacaoEditar.vao = formEditar.vao;
          localizacaoEditar.localizacaoId = this.editarId;

          if(this.produtoAlocado){
            localizacaoEditar.produtoId = this.produtoAlocado.produtoId
          }

          this.localizacaoService.editarLocalizacao(localizacaoEditar, this.editarId).subscribe({
            next: res => { 
              if(res){
                if(res.sucesso){
                  this.alertaService.sucesso(res.mensagem)
                }
                else{
                  this.alertaService.erro(res.mensagem)
                }
              }
            }
          });

        break;
      case 'DELETE':
          this.localizacaoService.removerLocalizacao(this.removerId).subscribe({
            next: res => { 
              if(res){
                if(res.sucesso){
                  this.alertaService.sucesso(res.mensagem)
                }
                else{
                  this.alertaService.erro(res.mensagem)
                }
              }
            }
          });
          this.fecharModal('deletarLocalizacaoModal');
          break;
      default:break; 
    }
  }

 
}
