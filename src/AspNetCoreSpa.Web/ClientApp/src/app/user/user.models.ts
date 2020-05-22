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
    startDatePro: Date,
    endDatePro: Date,
    touristType:number
}
interface Evaluation{
    id:string,
    oneStar:number,
    twoStar:number,
    threeStar:number,
    fourStar:number,
    fiveStar:number
}
interface TourProgram {
    id:string,
    orderNumber:number,
    title:string,
    description:string,
    destination:string,
    date:Date,
    tourId:string,
    tourName: string,
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
    id:string,
    tourBookingId:string,
    fullName:string,
    gender:boolean,
    birthDay:number,
    touristType:number,
    value:number
}
interface ImageTour {
    tourId:string,
    image:string
}
interface BookingPrice {
    tourBookingId:string,
    touristType:number,
    price:number
}
interface PageNum {
    num:number,
    active:string
}
interface EmitSearch{
    departureName:string,
    destinationName:string,
    option:OptionSearch
}
interface OptionSearch {
    departureId:string,
    destinationId:string,
    departureDateTimeStamp:string,
    tourCategoryId:string,
    priceId:number,
}
interface EmitSearchPost{
    departureName:string,
    destinationName:string,
    option:OptionSearchPost
}
interface OptionSearchPost {
    postCategoryId:string,
}
interface Province {
    id:string,
    name:string
}
interface Tourcate {
    id:string,
    name:string
}
interface SearchPrice {
    id:number,
    value:string
}
interface Post{
    id:string,
    name:string
    image:string,
    description:string,
    postContent:string,
    metaDescription:string,
    metaKeyWord:string,
    alias:string,
    createdAt:Date,
    forEach(param: (d, i) => void): void;
}
interface PostId{
    id:string,
    name:string
    image:string,
    description:string,
    postContent:string,
    metaDescription:string,
    metaKeyWord:string,
    alias:string,
    createdAt:Date,
}
interface PostCate{
    id:string,
    name:string
    forEach(param: (d, i) => void): void;
}