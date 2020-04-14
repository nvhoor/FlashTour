
interface Newest {
    id:string,
    name:string,
    image:string,
    description:string,
    departureDate:Date,
    departureId:string,
    slot:number,
    originalPrice:number,
    promotionPrice:number,
    startDatePro: Date,
    endDatePro: Date,
    touristType:number
}
interface Hotest {
    id:string,
    name:string,
    image:string,
    description:string,
    departureDate:Date,
    departureId:string,
    slot:number,
    originalPrice:number,
    promotionPrice:number,
    startDatePro: Date,
    endDatePro: Date,
    touristType:number
}
interface HotestGroup {
    tourId:string,
    tours:Array<Hotest>,
    count:number
}
interface TourCatePagings{
    tourCate:Array<TourCategory>,
    active:string
}
interface TourPagings{
    tours:Array<TourCard>,
    pageNum:number
}
interface TourCategory {
    id:string,
    name:string,
    description:string,
    image:string
}
interface Banner {
    id:string,
    name:string,
    description:string,
    image:string
    active:string
}