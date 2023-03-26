import { Injectable } from '@angular/core';
import { BehaviorSubject, find, Observable } from 'rxjs';
import { Alerta, TipoAlerta } from './alerta';

@Injectable({
  providedIn: 'root'
})
export class AlertaService {

  alertaSubject = new BehaviorSubject<Alerta>(new Alerta());
  
  sucesso(mensagem: string, opcoes?: any) {
    this.alerta(new Alerta({ ...opcoes, mensagem, tipo: TipoAlerta.Sucesso }));
  }

  erro(mensagem: string, opcoes?: any) {
    this.alerta(new Alerta({ ...opcoes, mensagem, tipo: TipoAlerta.Erro }));
  }

  info(mensagem: string, opcoes?: any) {
    this.alerta(new Alerta({ ...opcoes, mensagem, tipo: TipoAlerta.Info }));
  }

  aviso(mensagem: string, opcoes?: any) {
    this.alerta(new Alerta({ ...opcoes, mensagem, tipo: TipoAlerta.Aviso }));
  }

  // main alert method    
  alerta(alerta: Alerta) {
    this.alertaSubject.next(alerta);
  }

  // clear alerts
  limpar() {
    this.alertaSubject.next(new Alerta());
  }
}
