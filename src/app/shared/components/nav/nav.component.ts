import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  usuarioLogado = localStorage.getItem("nomecompleto");

  constructor(private router : Router) { }

  ngOnInit(): void {
  }

  sair():void{
    localStorage.clear();
    this.usuarioLogado = null;
    this.router.navigateByUrl('/usuario/login');
  }

}
