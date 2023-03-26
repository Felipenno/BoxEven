import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Alerta, TipoAlerta } from './alerta';
import { AlertaService } from './alerta.service';

@Component({
  selector: 'alerta',
  templateUrl: './alerta.component.html',
  styleUrls: ['./alerta.component.scss']
})
export class AlertaComponent implements OnInit, OnDestroy {

  alerta: Alerta = new Alerta();
  exibirAlerta = false;
  alertaSubscription: Subscription = new Subscription();

  constructor(private alertaService: AlertaService) { }

  ngOnInit() {
    this.alertaSubscription.add(this.alertaService.alertaSubject.subscribe(alerta => {
      if (!alerta.mensagem) {
        this.exibirAlerta = false
        this.alerta = new Alerta()
        return;
      }

      this.alerta = alerta;
      this.exibirAlerta = true

      if (alerta.fecharAutomatico) {
        setTimeout(() => this.removerAlerta(), alerta.tempoParaFechar);
      }
    }))
  }

  ngOnDestroy() {
    this.alertaSubscription.unsubscribe();
  }

  removerAlerta() {
    this.alerta = new Alerta()
    this.exibirAlerta = false
  }

  alertaClass(): any {

    if (!this.alerta.mensagem) return;

    switch (this.alerta.tipo) {
      case TipoAlerta.Sucesso: return 'alerta alerta-sucesso'
      case TipoAlerta.Info: return 'alerta alerta-info'
      case TipoAlerta.Aviso: return 'alerta alerta-aviso'
      case TipoAlerta.Erro: return 'alerta alerta-erro'
      default: this.removerAlerta(); break;
    }
  }
}