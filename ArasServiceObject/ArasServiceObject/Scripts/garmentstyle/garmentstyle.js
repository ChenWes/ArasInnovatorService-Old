//页面初始化
$(function () {
    var vm = new ViewModel();
    ko.applyBindings(vm);


    //分页控件加载处理
    //$.jqPaginator('#personPagination', {
    //    totalPages: totalPage,
    //    visiblePages: 10,
    //    currentPage: currentPage,
    //    onPageChange: function (num, type) {
    //        if (type != 'init') {
    //            window.location.href = '/part/Index?page=' + num;
    //        }
    //    }
    //});
});

//定义ViewModel对象
var ViewModel = function () {
    var self = this;

    //添加动态监视数组对象
    self.part = ko.observableArray(data);

    //编辑
    self.edit = function (obj) {
        window.location.href = '/GarmentStyle/Edit/' + obj.part_number;
    };

    //删除
    self.remove = function (obj) {
        $.ajax({
            type: "post",
            url: "/GarmentStyle/Del/" + obj.part_number,
            dataType: "json",
            success: function (json) {
                alert(json.result);
                self.persons.remove(obj);
            }
        })
    }
};