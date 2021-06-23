<template>
  <div id="app">
    <div class="text-blue-700 mt-40 ml-40 mr-40 mb-40">
      This is just presenting the task in a certain way. I am aware of many shortcuts and imperfections, but nevertheless it is a presentation of an idea. 
      The whole thing was built using microservices in .net, RabbitMq, Vue and I used the boilerplate (which is still being passively created in my free time) 
      of my authorship for the solutions. I was froced to improvise for a little bit, in fact notiffication service is not authenticated and notifications are shown for every user 
      of the app, but be real, there wasn't a lot of time.
    </div>
    <div class="text-blue-700 mt-40 ml-40 mr-40 mb-40">
      For every minute fraud service checks if there are any orders with state "new" so be patient :). All errors and notifications are being shown in right upper corner.
    </div>
    <div
      class="flex h-screen bg-gray-200 items-center justify-center"
      autocomplete="off"
    >
      <div
        class="
          grid
          bg-white
          rounded-lg
          shadow-xl
          w-11/12
          md:w-9/12
          lg:w-1/2
          pt-16
          pb-4
        "
      >
        <div class="flex justify-center">
          <div class="flex">
            <h1 class="text-gray-600 font-bold md:text-2xl text-xl">
              Create order
            </h1>
          </div>
        </div>

        <div class="grid grid-cols-1 mt-5 mx-7">
          <label
            class="
              uppercase
              md:text-sm
              text-xs text-gray-500 text-light
              font-semibold
            "
            >Email</label
          >
          <input
            v-model="order.email"
            class="
              py-2
              px-3
              rounded-lg
              border-2 border-purple-300
              mt-1
              focus:outline-none
              focus:ring-2 focus:ring-purple-600
              focus:border-transparent
            "
            type="text"
            placeholder="Email"
          />
        </div>

        <div class="grid grid-cols-1 md:grid-cols-2 gap-5 md:gap-8 mt-5 mx-7">
          <div class="grid grid-cols-1">
            <label
              class="
                uppercase
                md:text-sm
                text-xs text-gray-500 text-light
                font-semibold
              "
              >Amount</label
            >
            <input
              class="
                py-2
                px-3
                rounded-lg
                border-2 border-purple-300
                mt-1
                focus:outline-none
                focus:ring-2 focus:ring-purple-600
                focus:border-transparent
              "
              type="number"
              placeholder="Amount"
              v-model="order.amount"
            />
          </div>
          <div class="grid grid-cols-1">
            <label
              class="
                uppercase
                md:text-sm
                text-xs text-gray-500 text-light
                font-semibold
              "
              >Street</label
            >
            <input
              class="
                py-2
                px-3
                rounded-lg
                border-2 border-purple-300
                mt-1
                focus:outline-none
                focus:ring-2 focus:ring-purple-600
                focus:border-transparent
              "
              type="text"
              placeholder="Street"
              v-model="order.address.street"
            />
          </div>
          <div class="grid grid-cols-1">
            <label
              class="
                uppercase
                md:text-sm
                text-xs text-gray-500 text-light
                font-semibold
              "
              >Country</label
            >
            <input
              class="
                py-2
                px-3
                rounded-lg
                border-2 border-purple-300
                mt-1
                focus:outline-none
                focus:ring-2 focus:ring-purple-600
                focus:border-transparent
              "
              type="text"
              placeholder="Country"
              v-model="order.address.country"
            />
          </div>

          <div class="grid grid-cols-1">
            <label
              class="
                uppercase
                md:text-sm
                text-xs text-gray-500 text-light
                font-semibold
              "
              >City</label
            >
            <input
              class="
                py-2
                px-3
                rounded-lg
                border-2 border-purple-300
                mt-1
                focus:outline-none
                focus:ring-2 focus:ring-purple-600
                focus:border-transparent
              "
              type="text"
              placeholder="City"
              v-model="order.address.city"
            />
          </div>

          <div class="grid grid-cols-1">
            <label
              class="
                uppercase
                md:text-sm
                text-xs text-gray-500 text-light
                font-semibold
              "
              >Zip code</label
            >
            <input
              class="
                py-2
                px-3
                rounded-lg
                border-2 border-purple-300
                mt-1
                focus:outline-none
                focus:ring-2 focus:ring-purple-600
                focus:border-transparent
              "
              type="number"
              placeholder="Zip code"
              v-model="order.address.zipCode"
            />
          </div>
        </div>

        <div class="grid-cols-1 pl-40 pr-40 pt-10 pb-10">
          <div
            v-for="product in order.products"
            :key="product.id"
            class="grid grid-cols-3"
          >
            <div class="grid grid-cols-1">
              <label
                class="
                  uppercase
                  md:text-sm
                  text-xs text-gray-500 text-light
                  font-semibold
                "
                >Product name</label
              >
              <input
                class="
                  py-2
                  px-3
                  rounded-lg
                  border-2 border-purple-300
                  mt-1
                  focus:outline-none
                  focus:ring-2 focus:ring-purple-600
                  focus:border-transparent
                "
                type="text"
                placeholder="Product name"
                v-model="product.name"
              />
            </div>

            <div class="grid grid-cols-1">
              <label
                class="
                  uppercase
                  md:text-sm
                  text-xs text-gray-500 text-light
                  font-semibold
                "
                >Quantity</label
              >
              <input
                class="
                  ml-3
                  py-2
                  px-3
                  rounded-lg
                  border-2 border-purple-300
                  mt-1
                  focus:outline-none
                  focus:ring-2 focus:ring-purple-600
                  focus:border-transparent
                "
                type="text"
                placeholder="Quantity"
                v-model="product.quantity"
              />
            </div>

            <div class="grid grid-cols-1 ml-3">
              <button
                class="
                  w-auto
                  bg-green-500
                  hover:bg-green-700
                  rounded-lg
                  shadow-xl
                  font-medium
                  text-white
                  px-4
                  py-2
                "
                @click="appendWithNewProduct($event)"
              >
                Add next product
              </button>
            </div>
          </div>
        </div>

        <div class="flex items-center justify-center md:gap-8 gap-4 pt-5 pb-5">
          <button
            class="
              w-auto
              bg-gray-500
              hover:bg-gray-700
              rounded-lg
              shadow-xl
              font-medium
              text-white
              px-4
              py-2
            "
            @click="cleanOrder()"
          >
            Clear
          </button>
          <button
            class="
              w-auto
              bg-purple-500
              hover:bg-purple-700
              rounded-lg
              shadow-xl
              font-medium
              text-white
              px-4
              py-2
            "
            type="submit"
            @click="postOrder()"
          >
            Create
          </button>
        </div>
      </div>
    </div>
    <notifications group="notify" />
  </div>
</template>

<script>
export default {
  name: "App",
  components: {},
  data() {
    return {
      order: {
        email: "dummy@mail.com",
        amount: 230,
        address: {
          street: "Test",
          country: "Nigeria",
          city: "Testland",
          zipCode: 24592,
        },
        products: [
          {
            id: 0,
            name: "Dummy",
            quantity: 1,
          },
        ],
      },
    };
  },

  mounted() {
    this.$notificationHub.$on("Notification", (t) => {
      switch (t.type) {
        case 0:
          this.$notify({
            group: "notify",
            title: "Information",
            text: t.message,
          });
          break;
        case 1:
          this.$notify({
            type: "warning",
            group: "notify",
            title: "Warning",
            text: t.message,
          });
          break;
        case 2:
          this.$notify({
            type: "error",
            group: "notify",
            title: "Error",
            text: t.message,
          });
          break;
        case 3:
          this.$notify({
            type: "success",
            group: "notify",
            title: "Success",
            text: t.message,
          });
          break;

        default:
          break;
      }
    });
  },

  methods: {
    postOrder() {
      this.$axios
        .post("https://localhost:5001/Order", this.order)
        .then((res) => {
          console.log(res);
        })
        .catch((err) => {
          console.error(err);
        });

      this.cleanOrder();
    },

    cleanOrder() {
      this.order = {
        email: "",
        amount: 0,
        address: {
          street: "",
          country: "",
          city: "",
          zipCode: 0,
        },
        products: [
          {
            id: 0,
            name: "",
            quantity: 1,
          },
        ],
      };
    },

    appendWithNewProduct(event) {
      event.srcElement.hidden = true;
      this.order.products.push({
        id: Math.floor(Math.random() * 10000),
        name: "",
        quantity: 1
      })
    }
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
