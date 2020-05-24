import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {DataService} from "@app/services";
import {FormsService} from "@app/shared";
import {DOCUMENT} from "@angular/common";

@Component({
  selector: 'appc-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit {
  public post:Post;
  postsByCate: Post[];
  constructor(@Inject("BASE_URL") public baseUrl: string,
              private route: ActivatedRoute,
              private _renderer2: Renderer2,
              private _dataService:DataService,
              private formsService: FormsService,
              @Inject(DOCUMENT) private _document: Document) { 
    
  }
  
  ngOnInit() {
    var id=this.route.snapshot.params['id'];
    this.getPostById(id);
    this.getPostSameCate(id);
  }

  private getPostById(id) {
    var data = this._dataService.get<Post>(`${this.baseUrl}api/post/${id}`);
    let that = this;
    data.subscribe((result) => {
      that.post = result;
      // @ts-ignore
      console.log("Tour detail:"+JSON.stringify(that.tour));
      });
    }
  private getPostSameCate(id) {
    var data = this._dataService.getFull<Post[]>(`${this.baseUrl}api/post/postsSameCate/${id}`);
    let that = this;
    data.subscribe((result) => {
      // console.log("Respone:"+result.body);
      let postsByCate = [];
      result.body.forEach((d, i) => {
        postsByCate.push({
          id: d.id,
          name: d.name,
          description: d.description,
          image: d.image,
          postContent: d.postContent,
          metaDescription: d.metaDescription,
          metaKeyWord: d.metaKeyWord,
          alias: d.alias,
          createdAt: d.createdAt,
        });
      });
      that.postsByCate = postsByCate;
      console.log(that.postsByCate);
    }, error => console.error(error));
  }
  redirectMySelf(id) {
    this.getPostById(id);
  }
}
