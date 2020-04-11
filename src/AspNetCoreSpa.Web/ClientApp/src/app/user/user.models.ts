interface Tour {
    id:string,
    name:string,
    image:string,
    images:string,
    description:string,
    departureDate:Date,
    departureId:string,
    departureName:string,
    slot:number,
    viewCount:number,
    daysLeft:string,
    censorship:boolean,
    status:boolean,
    delete:boolean,
    tourCategoryId:string,
    originalPrice:number,
    promotionPrice:number,
    startDatePro:Date,
    endDatePro:Date
    tourPrograms:TourProgram[],
    prices:Price[]
}
interface TourCard {
    id:string,
    name:string,
    image:string,
    description:string,
    departureDate:Date,
    departureId:string,
    slot:number,
    originalPrice:number,
    promotionPrice:number,
    startDatePro:Date,
    touristType:number
}
interface TourProgram {
    id:string,
    orderNumber:number,
    title:string,
    description:string,
    destination:string,
    date:Date
}
interface CarouselImage {
    index:number,
    name:string,
    active:string
}
interface Price {
    tourId:string,
    name:string,
    originalPrice:number,
    promotionPrice:number,
    startDatePro:Date,
    endDatePro:Date,
    touristType:number
}
interface Comunication {
    fullName:string,
    email:string,
    mobile:string,
    address:string,
    adult:number,
    child:number,
    kid:number,
    note:string
    tourId:string,
    bookingPrices:Price[],
    tourCustomers:Customer[]
}
interface Customer {
    fullName:string,
    gender:boolean,
    birthday:Date,
    touristType:number,
    value:number
}
interface PageNum {
    num:number,
    active:string
}