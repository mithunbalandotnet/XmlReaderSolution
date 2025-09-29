import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReadXmlService } from '../services/read-xml-service';
import { Product } from '../model/product';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { error } from 'node:console';

@Component({
  selector: 'app-mail-content',
  imports: [
    FormsModule, CommonModule
],
  templateUrl: './mail-content.html',
  styleUrl: './mail-content.scss'
})
export class MailContent {
  content: string = 'THis is a test';
  result: Product | null = null;
  errorMessage:string | null = null;
  constructor(private xmlService: ReadXmlService, private toastr: ToastrService) {
    
  }

  clearContent() {
    console.log("Clearing content");
    this.content = '';
    this.result = null;
    this.errorMessage = null;
  }

  submitContent() {
    this.result = null;
    this.errorMessage = null;
    console.log("Submitted content:", this.content);
    this.xmlService.parseMailContent(this.content).subscribe({ 
      next: (result) => {
      this.result = result;
      this.errorMessage = null;
    }, error : (err) => {
      console.log(err);
      if(err && err.error && err.error.error){
        this.errorMessage = err.error.error;
      } else {
        this.errorMessage = 'An error occurred while processing the XML.';
      }
      this.result = null;
    }});
  }
}
