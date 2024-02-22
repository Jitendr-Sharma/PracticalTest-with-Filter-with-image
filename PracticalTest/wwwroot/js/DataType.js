$(document).ready(function () {
    $('#DataTable').DataTable({

    });

    /*GetProductList();*/
});
    function GetProductList() {
        $.ajax({
            url: '/Products/GetProductList',
            type: 'Get',
            dataType: 'json',
            success: OnSuccess

            
        })
}
function OnSuccess(respons) {
    debugger
    $('#DataTable').DataTable({
        bProcessing: true,
        bLengthChange: true,
        lengthMenu: [[5, 10, 15, 25, -1], [5, 10, 15, "All"]],
        bfilter: true,
        bSort: true,
        bPaginate: true,
        data: respons,
        columns: [
            {
                data: 'Id',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'Name',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'Detail',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'Picture',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'Price',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'Quantity',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'TotalPrice',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'CreatedDate',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
          
        ]

    });
}
